import { Component, Output, EventEmitter, Input, OnInit } from "@angular/core";
import { AbstractBoardGameComponent } from "../abstractBoardGame/abstractBoardGame.component";
import { Player } from "../../models/player/player.model";
import { GameService } from "../../services";

@Component({
    selector: "tile",
    templateUrl: "./tile.component.html",
    styleUrls: ["./tile.component.styles.css"]
})

export class TileComponent extends AbstractBoardGameComponent {

    constructor(private gameService: GameService) {
        super();
    }

    makeMove($event: any): any {
        this.owner = this.gameService.getCurrentPlayer();
        this.moveEvent.emit(null);
    }

}
