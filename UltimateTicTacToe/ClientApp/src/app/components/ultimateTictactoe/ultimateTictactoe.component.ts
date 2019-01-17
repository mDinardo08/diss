import { Component, Input, OnInit } from "@angular/core";
import { BoardGameComponent } from "../boardGame/boardGame.interface";
import { AbstractBoardGameComponent } from "../abstractBoardGame/abstractBoardGame.component";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import { Player } from "../../models/player/player.model";

@Component({
    selector: "ultimateTictactoe",
    templateUrl: "./ultimateTictactoe.component.html",
    styleUrls: ["./ultimateTictactoe.component.styles.css"]
})

export class TictactoeComponent extends AbstractBoardGameComponent implements OnInit {
    @Input() board: Array<Array<BoardGame>>;
    @Input() owner: Player;

    ngOnInit() {
        console.log(this.board);
    }

    setBoard(board: BoardGameComponent[][]): void {
        throw new Error("Method not implemented.");
    }

    getBoard(row: number, col: number): BoardGame {
        return this.board[row][col];
    }

    getColumn(col: number): Array<BoardGame> {
        const result = new Array<BoardGame>();
        this.board.forEach((row) => {
            result.push(row[col]);
        });
        return result;
    }
}
