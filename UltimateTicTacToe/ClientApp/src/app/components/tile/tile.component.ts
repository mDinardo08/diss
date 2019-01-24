import { Component, OnInit } from "@angular/core";
import { AbstractBoardGameComponent } from "../abstractBoardGame/abstractBoardGame.component";
import { AbstractGameService } from "../../services";

@Component({
    selector: "tile",
    templateUrl: "./tile.component.html",
    styleUrls: ["./tile.component.styles.css"]
})

export class TileComponent extends AbstractBoardGameComponent implements OnInit {

    public colour: string;

    constructor(private gameService: AbstractGameService) {
        super();
    }

    makeMove($event: any): any {
        this.owner = this.gameService.getCurrentPlayer();
        this.setColour();
        this.moveEvent.emit(null);
    }

}
