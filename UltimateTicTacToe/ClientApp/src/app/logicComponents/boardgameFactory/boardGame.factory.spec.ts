import { BoardGameFactory } from "./boardGame.factory";
import { TileComponent } from "../../components/tile/tile.component";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import { TictactoeComponent } from "../../components/ultimateTictactoe/ultimateTictactoe.component";
import { BoardGameComponent } from "../../components/boardGame/boardGame.interface";
import { Player } from "../../models/player/player.model";

describe("Board game factory", () => {

    let factory: BoardGameFactory;
    let mockResolver;
    let mockInjector;
    let mockCompFactory;
    let mockComp;
    beforeEach(() => {
        mockResolver = jasmine.createSpyObj("componentFactoryResolver", ["resolveComponentFactory"]);
        mockInjector = jasmine.createSpyObj("Injector", ["get"]);
        mockCompFactory = jasmine.createSpyObj("componentFactoryResolver", ["create"]);
        const mockCompRef = jasmine.createSpyObj("ComponentRef", ["instance"]);
        mockComp = jasmine.createSpyObj("BoardGameCompenent", ["setBoard", "setOwner"]);
        mockCompRef.instance = mockComp;
        mockCompFactory.create.and.returnValue(mockCompRef);
        factory = new BoardGameFactory(mockResolver, mockInjector);
    });

    it("Will pass type of TileComponent to component factory resolver", () => {
        mockResolver.resolveComponentFactory.and.returnValue(mockCompFactory);
        const boardGame = new BoardGame();
        factory.createBoardgame(boardGame);
        expect(mockResolver.resolveComponentFactory).toHaveBeenCalledWith(TileComponent);
    });

    it("Will pass type of tic tac toe component type to the component factory resolver if it has a board", () => {
        mockResolver.resolveComponentFactory.and.returnValue(mockCompFactory);
        const boardGame = new BoardGame();
        boardGame.board = new Array<Array<BoardGame>>();
        factory.createBoardgame(boardGame);
        expect(mockResolver.resolveComponentFactory).toHaveBeenCalledWith(TictactoeComponent);
    });

    it("Will call to create the component with the passed injector", () => {
        mockResolver.resolveComponentFactory.and.returnValue(mockCompFactory);
        const boardGame = new BoardGame();
        boardGame.board = new Array<Array<BoardGame>>();
        factory.createBoardgame(boardGame);
        expect(mockCompFactory.create).toHaveBeenCalledWith(mockInjector);
    });

    it("Will call to create a new board if type has a board", () => {
        spyOn(factory, "createBoardgame").and.callThrough();
        mockResolver.resolveComponentFactory.and.returnValue(mockCompFactory);
        const boardGame = new BoardGame();
        boardGame.board = [[new BoardGame()]];
        factory.createBoardgame(boardGame);
        expect(mockCompFactory.create).toHaveBeenCalledTimes(2);
    });

    it("Will recursively call to create new boards", () => {
        spyOn(factory, "createBoardgame").and.callThrough();
        mockResolver.resolveComponentFactory.and.returnValue(mockCompFactory);
        const boardGame = new BoardGame();
        const innerGame = new BoardGame();
        innerGame.board = [[new BoardGame()]];
        boardGame.board = [[innerGame]];
        factory.createBoardgame(boardGame);
        expect(mockCompFactory.create).toHaveBeenCalledTimes(3);
    });

    it("Will call to create new boardgames across rows", () => {
        spyOn(factory, "createBoardgame").and.callThrough();
        mockResolver.resolveComponentFactory.and.returnValue(mockCompFactory);
        const boardGame = new BoardGame();
        boardGame.board = [[new BoardGame()], [new BoardGame()]];
        factory.createBoardgame(boardGame);
        expect(mockCompFactory.create).toHaveBeenCalledTimes(3);
    });

    it("Will call to create new boardgames across columns", () => {
        spyOn(factory, "createBoardgame").and.callThrough();
        mockResolver.resolveComponentFactory.and.returnValue(mockCompFactory);
        const boardGame = new BoardGame();
        boardGame.board = [[new BoardGame(), new BoardGame()]];
        factory.createBoardgame(boardGame);
        expect(mockCompFactory.create).toHaveBeenCalledTimes(3);
    });

    it("Will call set board on the Boardgame component with the board it has built", () => {
        const boardStructure = new Array<Array<BoardGameComponent>>();
        spyOn(factory, "createBoardStructure").and.returnValue(boardStructure);
        mockResolver.resolveComponentFactory.and.returnValue(mockCompFactory);
        const boardGame = new BoardGame();
        boardGame.board = [[new BoardGame(), new BoardGame()]];
        factory.createBoardgame(boardGame);
        expect(mockComp.setBoard).toHaveBeenCalledWith(boardStructure);
    });

    it("Will call to set the owner of the board game", () => {
        const boardStructure = new Array<Array<BoardGameComponent>>();
        spyOn(factory, "createBoardStructure").and.returnValue(boardStructure);
        mockResolver.resolveComponentFactory.and.returnValue(mockCompFactory);
        const boardGame = new BoardGame();
        const player = new Player();
        boardGame.owner = player;
        factory.createBoardgame(boardGame);
        expect(mockComp.setOwner).toHaveBeenCalledWith(player);
    });
});
