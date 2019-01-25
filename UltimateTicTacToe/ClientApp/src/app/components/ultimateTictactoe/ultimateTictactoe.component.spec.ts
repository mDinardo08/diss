import { TictactoeComponent } from "./ultimateTictactoe.component";
import { Move } from "../../models/move/move.model";

describe("Tictactoe component", () => {

    let comp: TictactoeComponent;

    beforeEach(() => {
        comp = new TictactoeComponent();
    });

    it("Will emit a move event when it recieves one", () => {
        spyOn(comp.moveEvent, "emit");
        comp.moveMade(null, null, null);
        expect(comp.moveEvent.emit).toHaveBeenCalled();
    });

    it("Will emit a move object", () => {
        spyOn(comp.moveEvent, "emit");
        comp.moveMade(null, null, null);
        expect(comp.moveEvent.emit).toHaveBeenCalledWith(jasmine.any(Move));
    });

    it("Will emit a move whoes next property is the event it recieved", () => {
        spyOn(comp.moveEvent, "emit");
        const move = new Move();
        comp.moveMade(move, null, null);
        const event = new Move();
        event.next = move;
        event.possition = {
            X: null, Y: null
        };
        expect(comp.moveEvent.emit).toHaveBeenCalledWith(event);
    });

    it("Will assign a possition with the x being equal to the x passed in", ()  => {
        spyOn(comp.moveEvent, "emit");
        const move = new Move();
        comp.moveMade(null, 0, null);
        const event = new Move();
        event.possition = {X: 0, Y: null};
        event.next = null;
        expect(comp.moveEvent.emit).toHaveBeenCalledWith(event);
    });

    it("Will assign a possition with the y being equal to the y passed in", ()  => {
        spyOn(comp.moveEvent, "emit");
        const move = new Move();
        comp.moveMade(null, null, 0);
        const event = new Move();
        event.possition = {X: null, Y: 0};
        event.next = null;
        expect(comp.moveEvent.emit).toHaveBeenCalledWith(event);
    });

    it("Will return an empty list if available moves list is empty", () => {
        comp.availableMoves = [];
        const result = comp.getAvailableMoves(0, 0);
        expect(result).toEqual([]);
    });

    it("Will return moves next move if possition matches coords passed in", () => {
        const m = new Move();
        const n = new Move();
        m.next = n;
        m.possition = {
            X: 0,
            Y: 0
        };
        comp.availableMoves = [m];
        const result = comp.getAvailableMoves(0, 0);
        expect(result[0]).toBe(n);
    });

    it("Will return all moves that match in possition", () => {
        const m = new Move();
        const n = new Move();
        m.next = n;
        m.possition = {
            X: 0,
            Y: 0
        };
        comp.availableMoves = [m, m];
        const result = comp.getAvailableMoves(0, 0);
        expect(result.length).toBe(2);
    });
});
