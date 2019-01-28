import { Component } from "@angular/core";
import { AbstractBoardGameComponent } from "../abstractBoardGame/abstractBoardGame.component";
import { Move } from "../../models/move/move.model";

@Component({
    selector: "ultimateTictactoe",
    templateUrl: "./ultimateTictactoe.component.html",
    styleUrls: ["./ultimateTictactoe.styles.css"]
})

export class TictactoeComponent extends AbstractBoardGameComponent {
    moveMade($event: Move, x: number, y: number): void {
        const move = new Move();
        move.next = $event;
        move.possition = {
            x: x,
            y: y
        };
        this.moveEvent.emit(move);
    }

    getAvailableMoves(x: number, y: number): Array<Move> {
        let result = new Array<Move>();
        const filteredMoves = new Array<Move>();
        for (let i = 0; i < this.availableMoves.length; i++) {
            if (this.availableMoves[i].possition.x === x && this.availableMoves[i].possition.y === y) {
                filteredMoves.push(this.availableMoves[i]);
            }
        }
        result = result.concat(filteredMoves.map((move) => move.next));
        return result;
    }

    hasBorder(): boolean {
        let result = true;
        if (this.owner === null || this.owner === undefined) {
            for (let x = 0; x < this.board.length; x++) {
                for (let y = 0; y < this.board[x].length; y++) {
                    const subOwner = this.board[x][y].owner;
                    if (subOwner === null || subOwner === undefined) {
                        result = false;
                    }
                }
            }
        }
        return result;
    }

    getLastMove(x: number, y: number): Move {
        let result = null;
        if (this.lastMove !== null && this.lastMove !== undefined) {
            const possition = this.lastMove.possition;
            result = possition.x === x && possition.y === y ? this.lastMove.next : null;
        }
        return result;
    }


    hasTopHorizontalLine(x: number): boolean {
        return x > 0;
    }

    hasBottomHorizontalLine(x: number): boolean {
        return x < this.board.length - 1;
    }

    hasLeftVerticalLine(y: number): boolean {
        return y > 0;
    }

    hasRightVerticalLine(y: number): boolean {
        return y < this.board.length - 1;
    }
}
