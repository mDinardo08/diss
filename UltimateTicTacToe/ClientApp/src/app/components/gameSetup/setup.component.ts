import { Component, OnInit, EventEmitter, Output } from "@angular/core";
import { PlayerColour } from "../../models/player/player.colour.enum";
import { PlayerType } from "../../models/player/player.type.enum";
import { Player } from "../../models/player/player.model";

@Component({
    templateUrl: "./setup.component.html",
    selector: "setup"
})

export class GameSetupComponent implements OnInit {
    types = Object.keys(PlayerType);

    @Output() opponentSelectedEvent = new EventEmitter<Player>();

    ngOnInit() {
        this.types = this.types.slice(this.types.length / 2, this.types.length);
    }

    opponentSelected(typeName: string): void {
        const opponent = new Player();
        opponent.colour = PlayerColour.RED;
        opponent.type = PlayerType[typeName];
        opponent.name = "";
        opponent.userId = PlayerType[typeName];
        this.opponentSelectedEvent.emit(opponent);
    }
}
