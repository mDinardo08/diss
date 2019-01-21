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

    ngOnInit(): void {
        this.setColour();
    }

    makeMove($event: any): any {
        this.owner = this.gameService.getCurrentPlayer();
        this.setColour();
        this.moveEvent.emit(null);
    }

    private setColour(): void {
        if (this.owner !== undefined && this.owner !== null) {
            if (this.owner.colour === PlayerColour.BLUE) {
                this.colour = "#0275d8";
            } else {
                this.colour = "#d9534f";
            }
        }
    }

}
