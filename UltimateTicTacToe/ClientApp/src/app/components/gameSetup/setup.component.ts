import { Component, OnInit, EventEmitter, Output } from "@angular/core";
import { PlayerColour } from "../../models/player/player.colour.enum";
import { PlayerType } from "../../models/player/player.type.enum";
import { Player } from "../../models/player/player.model";
import { UserService } from "../../services";

@Component({
    templateUrl: "./setup.component.html",
    selector: "setup"
})

export class GameSetupComponent implements OnInit {
    types = Object.keys(PlayerType);
    opponentFirst = false;
    players: Array<Player>;
    type: string;
    @Output() opponentSelectedEvent = new EventEmitter<Array<Player>>();

    constructor (private userService: UserService) {}

    ngOnInit() {
        this.types = this.types.slice(this.types.length / 2, this.types.length);
    }

    opponentSelected(typeName: string): void {
        this.type = typeName;
        const opponent = new Player();
        opponent.colour = PlayerColour.RED;
        opponent.type = PlayerType[typeName];
        opponent.name = "";
        opponent.userId = PlayerType[typeName];
        const human = new Player();
        human.type = PlayerType.HUMAN;
        human.colour = PlayerColour.BLUE;
        human.name = "";
        human.userId = this.userService.getUserId();
        this.players = new Array<Player>();
        if (this.opponentFirst) {
            this.players.push(opponent);
            this.players.push(human);
        } else {
            this.players.push(human);
            this.players.push(opponent);
        }
    }

    play(): void {
        if (this.players !== null && this.players !== undefined) {
            this.opponentSelectedEvent.emit(this.players);
        }
    }
}
