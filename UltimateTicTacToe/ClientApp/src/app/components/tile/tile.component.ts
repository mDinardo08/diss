import { Component, OnInit } from "@angular/core";
import { AbstractBoardGameComponent } from "../abstractBoardGame/abstractBoardGame.component";
import { GameService } from "../../services";
import { PlayerColour } from "../../models/player/player.colour.enum";

@Component({
    selector: "tile",
    templateUrl: "./tile.component.html",
    styleUrls: ["./tile.component.styles.css"]
})

export class TileComponent extends AbstractBoardGameComponent implements OnInit{

    public colour: string;

    constructor(private gameService: GameService) {
        super();
    }

    makeMove($event: any): any {
        this.owner = this.gameService.getCurrentPlayer();
        this.setColour();
        this.moveEvent.emit(null);
    }

}
