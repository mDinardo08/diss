import { Component, OnInit, ViewChild, ViewContainerRef, ComponentRef } from "@angular/core";
import { BoardGameComponent } from "../boardGame/boardGame.interface";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import { BoardGameFactory } from "../../logicComponents/boardgameFactory/boardGame.factory";
import { Player } from "../../models/player/player.model";

@Component({
    selector: "ultimateTictactoe",
    templateUrl: "./ultimateTictactoe.component.html"
})

export class TictactoeComponent implements BoardGameComponent {

    setOwner(owner: Player): void {
    }

    setBoard(board: Array<Array<BoardGameComponent>>): void {
    }

}
