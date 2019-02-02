import { Player } from "../../models/player/player.model";
import { Component, EventEmitter, Output } from "@angular/core";
import { PlayerColour } from "../../models/player/player.colour.enum";

@Component({
    selector: "gameOver",
    templateUrl: "./gameOver.component.html"
})

export class GameOverComponent {

    public Winner: PlayerColour;
    public colours = PlayerColour;
    @Output() playAgainEvent = new EventEmitter<boolean>();
    hasWinner(): boolean {
        return this.Winner !== null && this.Winner !== undefined;
    }

    playAgain(playAgain: boolean): void {
        this.playAgainEvent.emit(playAgain);
    }
}
