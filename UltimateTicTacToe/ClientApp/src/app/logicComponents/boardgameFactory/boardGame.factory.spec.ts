import { BoardGameFactory } from "./boardGame.factory";
import { TileComponent } from "../../components/tile/tile.component";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import { TictactoeComponent } from "../../components/ultimateTictactoe/ultimateTictactoe.component";

describe("Board game factory", () => {

    let factory: BoardGameFactory;

    beforeEach(() => {
        factory = new BoardGameFactory(null);
    });

    it("Will pass type of TileComponent to component factory resolver", () => {
        const mockResolver = jasmine.createSpyObj("componentFactoryResolver", ["resolveComponentFactory"]);
        mockResolver.resolveComponentFactory.and.returnValue(jasmine.createSpyObj("componentFactoryResolver", ["create"]));
        const boardGame = new BoardGame();
        factory = new BoardGameFactory(mockResolver);
        factory.createBoardgame(boardGame);
        expect(mockResolver.resolveComponentFactory).toHaveBeenCalledWith(TileComponent);
    });

    it("Will return the component factory returned bu the component resolver", () => {
        const mockCompFactory = jasmine.createSpyObj("ComponentFactory", ["create"]);
        const mockResolver = jasmine.createSpyObj("componentFactoryResolver", ["resolveComponentFactory"]);
        mockResolver.resolveComponentFactory.and.returnValue(mockCompFactory);
        const mockInjector = jasmine.createSpyObj("Injector", ["get"]);
        const boardGame = new BoardGame();
        factory = new BoardGameFactory(mockResolver);
        const result = factory.createBoardgame(boardGame);
        expect(result).toBe(mockCompFactory);
    });

    it("Will pass type of tic tac toe component type to the component factory resolver if it has a board", () => {
        const mockResolver = jasmine.createSpyObj("componentFactoryResolver", ["resolveComponentFactory"]);
        mockResolver.resolveComponentFactory.and.returnValue(jasmine.createSpyObj("componentFactoryResolver", ["create"]));
        const boardGame = new BoardGame();
        boardGame.board = new Array<Array<BoardGame>>();
        factory = new BoardGameFactory(mockResolver);
        factory.createBoardgame(boardGame);
        expect(mockResolver.resolveComponentFactory).toHaveBeenCalledWith(TictactoeComponent);
    });
});
