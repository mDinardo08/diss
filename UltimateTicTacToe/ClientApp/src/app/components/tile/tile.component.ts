import { Component, OnInit } from "@angular/core";
import { AbstractBoardGameComponent } from "../abstractBoardGame/abstractBoardGame.component";
import { AbstractGameService } from "../../services";
import { Move } from "../../models/move/move.model";

@Component({
    selector: "tile",
    templateUrl: "./tile.component.html",
    styleUrls: ["./tile.component.styles.css"]
})

export class TileComponent extends AbstractBoardGameComponent implements OnInit {

    constructor(private gameService: AbstractGameService) {
        super();
    }

    makeMove($event: any): any {
        if (this.availableMoves.length > 0) {
            this.owner = this.gameService.getCurrentPlayer().colour;
            this.setColour();
            this.moveEvent.emit(new Move());
        }
    }

}
