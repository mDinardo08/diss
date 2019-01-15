import { Component } from "@angular/core";
import { BoardGameComponent } from "../boardGame/boardGame.interface";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";

@Component({
    selector: "ultimateTictactoe",
    templateUrl: "./ultimateTictactoe.component.html"
})

export class TictactoeComponent implements BoardGameComponent {

    setBoard(board: BoardGame): void {
        throw new Error("Method not implemented.");
    }

}
