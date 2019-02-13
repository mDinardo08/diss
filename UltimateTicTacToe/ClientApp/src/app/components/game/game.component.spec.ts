import { GameComponent } from "./game.component";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import "rxjs/add/observable/of";
import "rxjs/add/observable/throw";
import { Observable } from "rxjs/Observable";
import { Player } from "../../models/player/player.model";
import { Move } from "../../models/move/move.model";
import { BoardGameDTO } from "../../models/DTOs/BoardGameDTO";
describe("Game Component", () => {

    let comp: GameComponent;
    let mockService;
    let mockBs;

    beforeEach(() => {
        mockService = jasmine.createSpyObj("GameService", ["createGame", "makeMove"]);
        mockBs = jasmine.createSpyObj("BsModalService", ["show"]);
        comp = new GameComponent(mockService, mockBs, null);
    });

    it("Will call the game service with the board ", () => {
        mockService.makeMove.and.returnValue(Observable.of(BoardGameDTO));
        const game = new BoardGame();
        const board = new Array<Array<BoardGame>>();
        game.board = board;
        comp.game = game;
        comp.moveMade(null);
        expect(mockService.makeMove).toHaveBeenCalledWith(null);
    });

    it("Will call the game service with the move event", () => {
        mockService.makeMove.and.returnValue(Observable.of(BoardGameDTO));
        const move = new Move();
        comp.game = new BoardGame();
        comp.moveMade(move);
        expect(mockService.makeMove).toHaveBeenCalledWith(move);
    });

    it("Will subscribe to the game service board updated event", () => {
        mockService = jasmine.createSpyObj("GameService", ["boardUpdatedEvent", "createGame"]);
        const mockEmittor = jasmine.createSpyObj("EventEmitter", ["subscribe"]);
        const notCalledEmittor = jasmine.createSpyObj("EvenEmitter", ["subscribe"]);
        mockService.boardUpdatedEvent = mockEmittor;
        mockService.gameOverEvent = notCalledEmittor;
        mockBs.show.and.returnValue({content: {opponentSelectedEvent: notCalledEmittor}});
        comp = new GameComponent(mockService, mockBs, null, null);
        comp.ngOnInit();
        expect(mockService.boardUpdatedEvent.subscribe).toHaveBeenCalled();
    });

    it("Will set the over lay when a move is made", () => {
        comp.moveMade(new Move());
        expect(comp.overlayVisable).toBe(true);
    });

});
