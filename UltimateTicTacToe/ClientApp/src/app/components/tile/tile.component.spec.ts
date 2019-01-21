import { TileComponent } from "./tile.component";
import { componentFactoryName, AssertNotNull } from "@angular/compiler";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import { Player } from "../../models/player/player.model";
import { PlayerColour } from "../../models/player/player.colour.enum";

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

    it("Will init the colour variable correctly if the owner has colour BLUE", () => {
        const player = new Player();
        player.colour = PlayerColour.BLUE;
        comp.owner = player;
        comp.ngOnInit();
        expect(comp.colour).toBe("#0275d8");
    });

    it("Will init the colour correctly if the owner has colour red", () => {
        const player = new Player();
        player.colour = PlayerColour.RED;
        comp.owner = player;
        comp.ngOnInit();
        expect(comp.colour).toBe("#d9534f");
    });

    it("Will not throw an exception if owner is not set", () => {
        let exceptionThrown = false;
        try {
            comp.ngOnInit();
        } catch (exception) {
            exceptionThrown = true;
        }
        expect(exceptionThrown).toBe(false);
    });

    it("Will set it's colour to that of the current player when a move is made", () => {
        const mockService = jasmine.createSpyObj("GameService", ["getCurrentPlayer"]);
        const player = new Player();
        player.colour = PlayerColour.BLUE;
        mockService.getCurrentPlayer.and.returnValue(player);
        comp = new TileComponent(mockService);
        comp.makeMove(null);
        expect(comp.colour).toBe("#0275d8");
    });
});
