import { GameService } from "./game.service";
import { Boardgame } from "../../models/boardGame/boardgame.model";
import { Observable } from "rxjs/Observable";
import { BoardGameDTO } from "../../models/DTOs/tictactoeDTO";

describe("Game Service tests", () => {

    let service: GameService;

    beforeEach(() => {
        service = new GameService(null);
    });

    it("Will call the api service to make a move", () => {
        const mockApi = jasmine.createSpyObj("ApiService", ["post"]);
        service = new GameService(mockApi);
        service.makeMove(null);
        expect(mockApi.post).toHaveBeenCalled();
    });

    it("Will call the api with the correct endpoint", () => {
        const mockApi = jasmine.createSpyObj("ApiService", ["post"]);
        service = new GameService(mockApi);
        service.makeMove(null);
        expect(mockApi.post).toHaveBeenCalledWith("makeMove", null);
    });

    it("Will call the api with a BoardgameDto with the game being the game passed in", () => {
        const mockApi = jasmine.createSpyObj("ApiService", ["post"]);
        service = new GameService(mockApi);
        const game = new Array<Array<Boardgame>>();
        service.makeMove(game);
        expect(mockApi.post).toHaveBeenCalledWith("makeMove", game);
    });

    it("Will return the object returned from the api", () => {
        const mockApi = jasmine.createSpyObj("ApiService", ["post"]);
        const obsv = new Observable<BoardGameDTO>();
        mockApi.post.and.returnValue(obsv);
        service = new GameService(mockApi);
        const game = new Array<Array<Boardgame>>();
        const result = service.makeMove(game);
        expect(result).toBe(obsv);
    });
});
