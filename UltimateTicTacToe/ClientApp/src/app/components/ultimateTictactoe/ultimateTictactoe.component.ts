import { Component, Input, OnInit } from "@angular/core";
import { AbstractBoardGameComponent } from "../abstractBoardGame/abstractBoardGame.component";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import { Player } from "../../models/player/player.model";
import { Move } from "../../models/move/move.model";

@Component({
    selector: "ultimateTictactoe",
    templateUrl: "./ultimateTictactoe.component.html"
})

export class TictactoeComponent extends AbstractBoardGameComponent {

    moveMade($event: Move, x: number, y: number) {
        const move = new Move();
        move.next = $event;
        move.possition = {
            X: x,
            Y: y
        };
        this.moveEvent.emit(move);
    }
}
