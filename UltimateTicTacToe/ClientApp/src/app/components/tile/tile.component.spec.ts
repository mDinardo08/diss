import { TileComponent } from "./tile.component";
import { componentFactoryName } from "@angular/compiler";

describe("Tile Component", () => {

    let comp: TileComponent;

    beforeEach(() => {
        comp = new TileComponent();
    });

    it("Will create a moveEvent on creation", () => {
        expect(comp.moveEvent).not.toBeNull();
    });

    it("Will emit an event on makeMove", () => {
        spyOn(comp.moveEvent, "emit");
        comp.makeMove();
        expect(comp.moveEvent.emit).toHaveBeenCalled();
    });
});
