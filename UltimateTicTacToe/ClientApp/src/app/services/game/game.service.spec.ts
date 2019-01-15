import { GameService } from "./game.service";
import { Observable } from "rxjs/Observable";
import { BoardGameDTO } from "../../models/DTOs/BoardGameDTO";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";

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
        expect(mockApi.post).toHaveBeenCalledWith("Game/makeMove", null);
    });

    it("Will call the api with a BoardgameDto with the game being the game passed in", () => {
        const mockApi = jasmine.createSpyObj("ApiService", ["post"]);
        service = new GameService(mockApi);
        const game = new Array<Array<BoardGame>>();
        service.makeMove(game);
        expect(mockApi.post).toHaveBeenCalledWith("Game/makeMove", game);
    });

    it("Will return the object returned from the api", () => {
        const mockApi = jasmine.createSpyObj("ApiService", ["post"]);
        const obsv = new Observable<BoardGameDTO>();
        mockApi.post.and.returnValue(obsv);
        service = new GameService(mockApi);
        const game = new Array<Array<BoardGame>>();
        const result = service.makeMove(game);
        expect(result).toBe(obsv);
    });

    it("Will call the api to get a new board", () => {
        const mockApi = jasmine.createSpyObj("ApiService", ["get"]);
        service = new GameService(mockApi);
        service.createGame(null);
        expect(mockApi.get).toHaveBeenCalled();
    });

    it("Will call the api with the correct end point", () => {
        const mockApi = jasmine.createSpyObj("ApiService", ["get"]);
        service = new GameService(mockApi);
        service.createGame(2);
        expect(mockApi.get).toHaveBeenCalledWith("Game/createBoard/2");
    });

    it("Will return the observable given by the api", () => {
        const mockApi = jasmine.createSpyObj("ApiService", ["get"]);
        const obvs = new Observable<BoardGame>();
        mockApi.get.and.returnValue(obvs);
        service = new GameService(mockApi);
        const result = service.createGame(null);
        expect(result).toBe(obvs);
    });
});
