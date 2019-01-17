import { Component, Input, OnInit } from "@angular/core";
import { AbstractBoardGameComponent } from "../abstractBoardGame/abstractBoardGame.component";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import { Player } from "../../models/player/player.model";

@Component({
    selector: "ultimateTictactoe",
    templateUrl: "./ultimateTictactoe.component.html"
})

export class TictactoeComponent extends AbstractBoardGameComponent {
    @Input() board: Array<Array<BoardGame>>;
    @Input() owner: Player;

}
