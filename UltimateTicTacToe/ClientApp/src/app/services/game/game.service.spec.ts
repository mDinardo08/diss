import { GameService } from "./game.service";
import { Observable } from "rxjs/Observable";
import { BoardGameDTO } from "../../models/DTOs/BoardGameDTO";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import { BoardCreationDTO } from "../../models/DTOs/BoardCreationDTO";
import { Player } from "../../models/player/player.model";

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
        const mockApi = jasmine.createSpyObj("ApiService", ["post"]);
        service = new GameService(mockApi);
        service.createGame(null, null);
        expect(mockApi.post).toHaveBeenCalled();
    });

    it("Will call the api with the correct size arguement", () => {
        const mockApi = jasmine.createSpyObj("ApiService", ["post"]);
        service = new GameService(mockApi);
        const dto = new BoardCreationDTO();
        dto.size = 2;
        dto.players = null;
        service.createGame(2, null);
        expect(mockApi.post).toHaveBeenCalledWith("Game/createBoard", dto);
    });

    it("Will call the api with the correct players arguement", () => {
        const mockApi = jasmine.createSpyObj("ApiService", ["post"]);
        service = new GameService(mockApi);
        const dto = new BoardCreationDTO();
        const players = [
            new Player()
        ];
        dto.size = null;
        dto.players = players;
        service.createGame(null, players);
        expect(mockApi.post).toHaveBeenCalledWith("Game/createBoard", dto);
    });

    it("Will return the observable given by the api", () => {
        const mockApi = jasmine.createSpyObj("ApiService", ["post"]);
        const obvs = new Observable<BoardGameDTO>();
        mockApi.post.and.returnValue(obvs);
        service = new GameService(mockApi);
        const result = service.createGame(null, null);
        expect(result).toBe(obvs);
    });
});
