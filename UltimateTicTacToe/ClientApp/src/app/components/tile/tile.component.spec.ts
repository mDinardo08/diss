import { TileComponent } from "./tile.component";
import { componentFactoryName, AssertNotNull } from "@angular/compiler";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import { Player } from "../../models/player/player.model";
import { PlayerColour } from "../../models/player/player.colour.enum";
import { Move } from "../../models/move/move.model";

describe("Tile Component", () => {

    let comp: TileComponent;

    beforeEach(() => {
        const mockService = jasmine.createSpyObj("GameService", ["getCurrentPlayer"]);
        comp = new TileComponent(mockService);
        comp.availableMoves = [];
    });

    it("Will create a moveEvent on creation", () => {
        expect(comp.moveEvent).not.toBeNull();
    });

    it("Will emit an event on makeMove if available moves is not empty", () => {
        comp.availableMoves.push(new Move());
        const mockService = jasmine.createSpyObj("GameService", ["getCurrentPlayer"]);
        const player = new Player();
        player.colour = 0;
        mockService.getCurrentPlayer.and.returnValue(player);
        comp = new TileComponent(mockService);
        spyOn(comp.moveEvent, "emit");
        comp.availableMoves = [new Move()];
        comp.makeMove(null);
        expect(comp.moveEvent.emit).toHaveBeenCalled();
    });

    it("Will emit a new Move", () => {
        const mockService = jasmine.createSpyObj("GameService", ["getCurrentPlayer"]);
        const player = new Player();
        player.colour = 0;
        mockService.getCurrentPlayer.and.returnValue(player);
        comp = new TileComponent(mockService);
        comp.availableMoves = [new Move()];
        spyOn(comp.moveEvent, "emit");
        comp.makeMove(null);
        expect(comp.moveEvent.emit).toHaveBeenCalledWith(jasmine.any(Move));
    });

    it("Will set its owner to whatever player the game service returns", () => {
        const mockService = jasmine.createSpyObj("GameService", ["getCurrentPlayer"]);
        const player = new Player();
        player.name = "fake";
        player.colour = 0;
        mockService.getCurrentPlayer.and.returnValue(player);
        comp = new TileComponent(mockService);
        comp.availableMoves = [new Move()];
        comp.makeMove(null);
        expect(comp.owner).toBe(0);
    });

    it("Will init the colour variable correctly if the owner has colour BLUE", () => {
        comp.owner = PlayerColour.BLUE;
        comp.ngOnInit();
        expect(comp.colour).toBe("#0275d8");
    });

    it("Will init the colour correctly if the owner has colour red", () => {
        const player = new Player();
        comp.owner = PlayerColour.RED;
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
        comp.availableMoves = [new Move()];
        comp.makeMove(null);
        expect(comp.colour).toBe("#0275d8");
    });

    it("Will not emit an event if available moves is empty array", () => {
        comp.availableMoves = [];
        spyOn(comp.moveEvent, "emit");
        comp.makeMove(null);
        expect(comp.moveEvent.emit).not.toHaveBeenCalled();
    });
});
