import { GameComponent } from "./game.component";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import "rxjs/add/observable/of";
import "rxjs/add/observable/throw";
import { Observable } from "rxjs/Observable";
import { Player } from "../../models/player/player.model";
import { Move } from "../../models/move/move.model";
describe("Game Component", () => {

    let comp: GameComponent;
    let mockService;

    beforeEach(() => {
        mockService = jasmine.createSpyObj("GameService", ["createGame", "makeMove"]);
        comp = new GameComponent(mockService);
    });

    it("Will call the game service with the board ", () => {
        const game = new BoardGame();
        const board = new Array<Array<BoardGame>>();
        game.board = board;
        comp.board = game;
        comp.moveMade(null);
        expect(mockService.makeMove).toHaveBeenCalledWith(board);
    });

});
