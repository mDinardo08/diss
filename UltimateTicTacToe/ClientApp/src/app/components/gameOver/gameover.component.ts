import { Player } from "../../models/player/player.model";
import { Component } from "@angular/core";
import { PlayerColour } from "../../models/player/player.colour.enum";

@Component({
    selector: "gameOver",
    templateUrl: "./gameOver.component.html"
})

export class GameOverComponent {

    public Winner: Player;
    public colours = PlayerColour;
    hasWinner(): boolean {
        return this.Winner !== null && this.Winner !== undefined;
    }
}
