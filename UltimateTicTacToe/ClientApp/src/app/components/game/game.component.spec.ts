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

    beforeEach(() => {
        mockService = jasmine.createSpyObj("GameService", ["createGame", "makeMove"]);
        comp = new GameComponent(mockService);
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

    it("Will set the board returned from the service as it's board", () => {
        const dto = new BoardGameDTO();
        dto.game = new Array<Array<BoardGame>>();
        mockService.makeMove.and.returnValue(Observable.of(dto));
        comp.game = new BoardGame();
        comp.moveMade(null);
        expect(comp.game.board).toBe(dto.game);
    });
});
