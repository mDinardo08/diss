import { TileComponent } from "./tile.component";
import { componentFactoryName } from "@angular/compiler";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import { Player } from "../../models/player/player.model";

describe("Tile Component", () => {

    let comp: TileComponent;

    beforeEach(() => {
        const mockService = jasmine.createSpyObj("GameService", ["getCurrentPlayer"]);
        comp = new TileComponent(mockService);
    });

    it("Will create a moveEvent on creation", () => {
        expect(comp.moveEvent).not.toBeNull();
    });

    it("Will emit an event on makeMove", () => {
        spyOn(comp.moveEvent, "emit");
        comp.makeMove(null);
        expect(comp.moveEvent.emit).toHaveBeenCalled();
    });

    it("Will emit null", () => {
        spyOn(comp.moveEvent, "emit");
        comp.makeMove(null);
        expect(comp.moveEvent.emit).toHaveBeenCalledWith(null);
    });

    it("Will set its owner to whatever player the game service returns", () => {
        const mockService = jasmine.createSpyObj("GameService", ["getCurrentPlayer"]);
        const player = new Player();
        player.name = "fake";
        mockService.getCurrentPlayer.and.returnValue(player);
        comp = new TileComponent(mockService);
        comp.makeMove(null);
        expect(comp.owner).toBe(player);
    });
});
