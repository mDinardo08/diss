import { Component } from "@angular/core";
import { BoardGameComponent } from "../boardGame/boardGame.interface";
import { AbstractBoardGameComponent } from "../abstractBoardGame/abstractBoardGame.component";

@Component({
    selector: "ultimateTictactoe",
    templateUrl: "./ultimateTictactoe.component.html"
})

export class TictactoeComponent extends AbstractBoardGameComponent {

    setBoard(board: Array<Array<BoardGameComponent>>): void {
    }

}
