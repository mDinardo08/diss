import { Component, OnInit, ViewChild, ViewContainerRef, ComponentRef } from "@angular/core";
import { BoardGameComponent } from "../boardGame/boardGame.interface";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import { BoardGameFactory } from "../../logicComponents/boardgameFactory/boardGame.factory";

@Component({
    selector: "ultimateTictactoe",
    templateUrl: "./ultimateTictactoe.component.html"
})

export class TictactoeComponent implements BoardGameComponent {

    setBoard(board: Array<Array<BoardGameComponent>>): void {
    }

}
