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
    type1: string;
    type2: string;
    @Output() opponentSelectedEvent = new EventEmitter<Array<Player>>();

    constructor (private userService: UserService) {}

    ngOnInit() {
        this.types = this.types.slice(this.types.length / 2, this.types.length);
        this.players = new Array<Player>(2);
    }

    player1Selected(typeName: string): void {
        this.type1 = typeName;
        const player = this.createPlayer(typeName);
        player.colour = PlayerColour.BLUE;
        this.players[0] = player;
    }

    player2Selected(typeName: string): void {
        this.type2 = typeName;
        const player = this.createPlayer(typeName);
        player.colour = PlayerColour.RED;
        this.players[1] = player;
    }

    private createPlayer(typeName: string): Player {
        const player = new Player();
        player.type = PlayerType[typeName];
        player.name = typeName;
        if (player.type === PlayerType.HUMAN) {
            player.userId = this.userService.getUserId();
        } else {
            player.userId = player.type;
        }
        return player;
    }

    play(): void {
        if (this.playersAreValid()) {
            this.opponentSelectedEvent.emit(this.players);
        }
    }

    playersAreValid(): boolean {
        let noPlayers = 0;
        this.players.forEach(x => {
            noPlayers++;
        });
        return noPlayers === 2;
    }
}
