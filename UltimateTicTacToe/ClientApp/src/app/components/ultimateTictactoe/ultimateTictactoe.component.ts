import { Component, Input, OnInit } from "@angular/core";
import { AbstractBoardGameComponent } from "../abstractBoardGame/abstractBoardGame.component";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import { Player } from "../../models/player/player.model";
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
            X: x,
            Y: y
        };
        this.moveEvent.emit(move);
    }

    getAvailableMoves(x: number, y: number): Array<Move> {
        let result = new Array<Move>();
        if (this.availableMoves.length > 0) {
            const filteredMoves = this.availableMoves.filter((move) => {
                return move.possition.X === x && move.possition.Y === y;
            });
            result = result.concat(filteredMoves.map((move) => move.next));
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
