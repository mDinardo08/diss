import { GameComponent } from "./game.component";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import "rxjs/add/observable/of";
import "rxjs/add/observable/throw";
import { Observable } from "rxjs/Observable";
describe("Game Component", () => {

    let comp: GameComponent;
    let mockService;
    let mockFactory;
    beforeEach(() => {
        mockService = jasmine.createSpyObj("GameService", ["createGame"]);
        mockFactory = jasmine.createSpyObj("BoardGameFactory", ["createBoardgame"]);
        comp = new GameComponent(mockService, mockFactory);
    });

    it("Will ask the game service for a new game on init", () => {
        mockService.createGame.and.returnValue(Observable.of("{}"));
        comp.ngOnInit();
        expect(mockService.createGame).toHaveBeenCalled();
    });

    it("Will default to a game size of 3", () => {
        mockService.createGame.and.returnValue(Observable.of("{}"));
        comp.ngOnInit();
        expect(mockService.createGame).toHaveBeenCalledWith(3);
    });

});
