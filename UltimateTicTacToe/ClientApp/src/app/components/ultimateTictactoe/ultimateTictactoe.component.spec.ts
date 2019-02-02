import { TictactoeComponent } from "./ultimateTictactoe.component";
import { Move } from "../../models/move/move.model";
import { Player } from "../../models/player/player.model";
import { AssertNotNull } from "@angular/compiler";

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
            x: null, y: null
        };
        expect(comp.moveEvent.emit).toHaveBeenCalledWith(event);
    });

    it("Will assign a possition with the x being equal to the x passed in", ()  => {
        spyOn(comp.moveEvent, "emit");
        const move = new Move();
        comp.moveMade(null, 0, null);
        const event = new Move();
        event.possition = {x: 0, y: null};
        event.next = null;
        expect(comp.moveEvent.emit).toHaveBeenCalledWith(event);
    });

    it("Will assign a possition with the y being equal to the y passed in", ()  => {
        spyOn(comp.moveEvent, "emit");
        const move = new Move();
        comp.moveMade(null, null, 0);
        const event = new Move();
        event.possition = {x: null, y: 0};
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
        n.possition = {
            x: 0,
            y: 0
        };
        m.next = n;
        m.possition = {
            x: 0,
            y: 0
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
            x: 0,
            y: 0
        };
        comp.availableMoves = [m, m];
        const result = comp.getAvailableMoves(0, 0);
        expect(result.length).toBe(2);
    });

    it("Will return all moves if the possition matches", () => {
        const internal = new Move();
        internal.next = internal;
        internal.possition =  { x: 1, y: 2};
        comp.availableMoves = [
            internal,
            internal,
            internal,
            internal,
            internal];
        const result = comp.getAvailableMoves(1, 2);
        expect(result.length).toBe(5);
    });

    it("Will return true if owner is set", () => {
        comp.owner = 0;
        expect(comp.hasBorder()).toBe(true);
    });

    it("Will return true if owner is not set but all subboards have a owner", () => {
        const sub = {owner: 0, board: [[]]};
        comp.board = [[sub]];
        expect(comp.hasBorder()).toBe(true);
    });

    it("Will return false if owner is not set and atleast one subboard does not have an owner", () => {
        const sub = {owner: null, board: [[]]};
        comp.board = [[sub]];
        expect(comp.hasBorder()).toBe(false);
    });

    it("Will return null if the x component does not match the x of the last move", () => {
        const move = new Move();
        move.possition = {
            x: 10, y: 0
        };
        comp.lastMove = move;
        expect(comp.getLastMove(0, 0)).toBeNull();
    });

    it("Will return null if the y component does not match the x of the last move", () => {
        const move = new Move();
        move.possition = {
            x: 0, y: 10
        };
        comp.lastMove = move;
        expect(comp.getLastMove(0, 0)).toBeNull();
    });

    it("Will return the next move if both x and y match", () => {
        const move = new Move();
        move.possition = {
            x: 0, y: 0
        };
        move.next = new Move();
        comp.lastMove = move;
        expect(comp.getLastMove(0, 0)).toBe(move.next);
    });

    it("Will not throw an exeption if lastmove is null", () => {
        try {
            comp.getLastMove(0, 0);
        } catch {
            fail();
        }
    });
});
