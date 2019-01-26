import { GameService } from "./game.service";
import { Observable } from "rxjs/Observable";
import { BoardGameDTO } from "../../models/DTOs/BoardGameDTO";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import { BoardCreationDTO } from "../../models/DTOs/BoardCreationDTO";
import { Player } from "../../models/player/player.model";
import { Move } from "../../models/move/move.model";

describe("Game Service tests", () => {

    let service: GameService;

    beforeEach(() => {
        service = new GameService(null);
    });

    it("Will assign the current player as the owner of the tile described on the move", () => {
        const board = new Array<Array<BoardGame>>();
        board[0] = new Array<BoardGame>();
        board[0][0] = new BoardGame();
        service.board = board;
        const move = new Move();
        move.possition = {x: 0, y: 0};
        move.next = new Move();
        service.curPlayer = new Player();
        const result = service.makeMoveOnBoard(board, move);
        expect(result[0][0].owner).toBe(service.curPlayer);
    });

    it("Will assign the current player of the owner of a tile across a row", () => {
        const board = new Array<Array<BoardGame>>();
        board[0] = new Array<BoardGame>();
        board[0][0] = new BoardGame();
        board[0][1] = new BoardGame();
        service.board = board;
        const move = new Move();
        move.possition = {x: 0, y: 1};
        move.next = new Move();
        service.curPlayer = new Player();
        const result = service.makeMoveOnBoard(board, move);
        expect(result[0][1].owner).toBe(service.curPlayer);
    });

    it("Will assign the current player as the owner of a tile down a column", () => {
        const board = new Array<Array<BoardGame>>();
        board[0] = new Array<BoardGame>();
        board[1] = new Array<BoardGame>();
        board[1][0] = new BoardGame();
        service.board = board;
        const move = new Move();
        move.next = new Move();
        move.possition = {x: 1, y: 0};
        service.curPlayer = new Player();
        const result = service.makeMoveOnBoard(board, move);
        expect(result[1][0].owner).toBe(service.curPlayer);
    });

    it("Will call to make a move with nested move", () => {
        const board = new Array<Array<BoardGame>>();
        board[0] = new Array<BoardGame>();
        const innerBoard = new BoardGame();
        innerBoard.board = new Array<Array<BoardGame>>();
        innerBoard.board[0] = new Array<BoardGame>();
        innerBoard.board[0][0] = new BoardGame();
        board[0][0] = innerBoard;
        service.board = board;
        const move = new Move();
        move.possition = {x: 0, y: 0};
        const innerMove = new Move();
        innerMove.possition = {x: 0, y: 0};
        innerMove.next = new Move();
        move.next = innerMove;
        service.curPlayer = new Player();
        spyOn(service, "makeMoveOnBoard").and.callThrough();
        service.makeMoveOnBoard(board, move);
        expect(service.makeMoveOnBoard).toHaveBeenCalledTimes(2);
    });

    it("Will call the api service to make a move", () => {
        const mockApi = jasmine.createSpyObj("ApiService", ["post"]);
        mockApi.post.and.returnValue(Observable.of(BoardGameDTO));
        service = new GameService(mockApi);
        spyOn(service, "makeMoveOnBoard").and.returnValue(new Array<Array<BoardGame>>());
        spyOn(service, "getNextPlayer").and.returnValue(new Player());
        service.makeMove(null);
        expect(mockApi.post).toHaveBeenCalled();
    });

    it("Will call the api with the correct endpoint", () => {
        const mockApi = jasmine.createSpyObj("ApiService", ["post"]);
        mockApi.post.and.returnValue(Observable.of(BoardGameDTO));
        service = new GameService(mockApi);
        spyOn(service, "makeMoveOnBoard").and.returnValue(new Array<Array<BoardGame>>());
        spyOn(service, "getNextPlayer").and.returnValue(new Player());
        const dto = new BoardGameDTO();
        dto.game = new Array<Array<BoardGame>>();
        dto.cur = new Player();
        dto.lastMove = null;
        dto.players = undefined;
        service.makeMove(null);
        expect(mockApi.post).toHaveBeenCalledWith("Game/makeMove", dto);
    });

    it("Will call the api with a BoardgameDto with the last move set", () => {
        const mockApi = jasmine.createSpyObj("ApiService", ["post"]);
        mockApi.post.and.returnValue(Observable.of(BoardGameDTO));
        service = new GameService(mockApi);
        spyOn(service, "makeMoveOnBoard").and.returnValue(new Array<Array<BoardGame>>());
        spyOn(service, "getNextPlayer").and.returnValue(new Player());
        const move = new Move();
        service.makeMove(move);
        const dto = new BoardGameDTO();
        dto.game = new Array<Array<BoardGame>>();
        dto.cur = new Player();
        dto.lastMove = move;
        dto.players = undefined;
        service.makeMove(null);
        expect(mockApi.post).toHaveBeenCalledWith("Game/makeMove", dto);
    });


    it("Will call the api to get a new board", () => {
        const mockApi = jasmine.createSpyObj("ApiService", ["post"]);
        mockApi.post.and.returnValue(Observable.of(BoardGameDTO));
        service = new GameService(mockApi);
        service.createGame(null, null);
        expect(mockApi.post).toHaveBeenCalled();
    });

    it("Will call the api with the correct size arguement", () => {
        const mockApi = jasmine.createSpyObj("ApiService", ["post"]);
        mockApi.post.and.returnValue(Observable.of(BoardGameDTO));
        service = new GameService(mockApi);
        const dto = new BoardCreationDTO();
        dto.size = 2;
        dto.players = null;
        service.createGame(2, null);
        expect(mockApi.post).toHaveBeenCalledWith("Game/createBoard", dto);
    });

    it("Will call the api with the correct players arguement", () => {
        const mockApi = jasmine.createSpyObj("ApiService", ["post"]);
        mockApi.post.and.returnValue(Observable.of(BoardGameDTO));
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

    it("Will set the current player to the cur player on the boardDto", () => {
        const mockApi = jasmine.createSpyObj("ApiService", ["post"]);
        const player = new Player();
        const dto = new BoardGameDTO();
        dto.cur = player;
        mockApi.post.and.returnValue(Observable.of(dto));
        service = new GameService(mockApi);
        service.createGame(null, null);
        expect(service.getCurrentPlayer()).toBe(player);
    });

    it("Will set the players list to that returned by the api", () => {
        const mockApi = jasmine.createSpyObj("ApiService", ["post"]);
        const player = new Player();
        const players = [player, player];
        const dto = new BoardGameDTO();
        dto.cur = player;
        dto.players = players;
        mockApi.post.and.returnValue(Observable.of(dto));
        service = new GameService(mockApi);
        service.createGame(null, null);
        expect(service.players).toBe(players);
    });

    it("Will return the player whos colour doesn't match the current player", () => {
        service.curPlayer = new Player();
        service.curPlayer.colour = 10;
        const other = new Player();
        other.colour = 1000;
        service.players = [service.curPlayer, other];
        const result = service.getNextPlayer();
        expect(result).toBe(other);
    });

    it("Will set the board returned on board creation as it's board", () => {
        const mockApi = jasmine.createSpyObj("ApiService", ["post"]);
        const dto = new BoardGameDTO();
        dto.game = new Array<Array<BoardGame>>();
        mockApi.post.and.returnValue(Observable.of(dto));
        service = new GameService(mockApi);
        service.createGame(null, null);
        expect(service.board).toBe(dto.game);
    });

    it("Will set the board returned after a move as it's board", () => {
        const mockApi = jasmine.createSpyObj("ApiService", ["post"]);
        const dto = new BoardGameDTO();
        dto.game = new Array<Array<BoardGame>>();
        mockApi.post.and.returnValue(Observable.of(dto));
        service = new GameService(mockApi);
        service.players = [];
        spyOn(service, "makeMoveOnBoard").and.returnValue(new Array<Array<BoardGame>>());
        service.makeMove(null);
        expect(service.board).toBe(dto.game);

    });

    it("Will emit an event when the board has been updated", () => {
        const mockApi = jasmine.createSpyObj("ApiService", ["post"]);
        const dto = new BoardGameDTO();
        dto.game = new Array<Array<BoardGame>>();
        mockApi.post.and.returnValue(Observable.of(dto));
        service = new GameService(mockApi);
        spyOn(service.boardUpdatedEvent, "emit");
        spyOn(service, "makeMoveOnBoard").and.returnValue(null);
        spyOn(service, "getNextPlayer").and.returnValue(null);
        service.makeMove(null);
        expect(service.boardUpdatedEvent.emit).toHaveBeenCalledWith(dto.game);
    });

    it("Will emit an event when the board has been created", () => {
        const mockApi = jasmine.createSpyObj("ApiService", ["post"]);
        const dto = new BoardGameDTO();
        dto.game = new Array<Array<BoardGame>>();
        mockApi.post.and.returnValue(Observable.of(dto));
        service = new GameService(mockApi);
        spyOn(service.boardUpdatedEvent, "emit");
        service.createGame(null, null);
        expect(service.boardUpdatedEvent.emit).toHaveBeenCalledWith(dto.game);
    });

    it("Will set the available moves to what the api had", () => {
        const mockApi = jasmine.createSpyObj("ApiService", ["post"]);
        const dto = new BoardGameDTO();
        dto.game = new Array<Array<BoardGame>>();
        dto.availableMoves = new Array<Move>();
        mockApi.post.and.returnValue(Observable.of(dto));
        service = new GameService(mockApi);
        spyOn(service, "makeMoveOnBoard").and.returnValue(null);
        spyOn(service, "getNextPlayer").and.returnValue(null);
        service.makeMove(null);
        expect(service.getAvailableMoves()).toBe(dto.availableMoves);
    });
});
