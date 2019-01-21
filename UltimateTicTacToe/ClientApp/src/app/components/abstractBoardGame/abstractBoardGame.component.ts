import { Player } from "../../models/player/player.model";
import { Input, Output, EventEmitter, OnInit } from "@angular/core";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import { Move } from "../../models/move/move.model";
import { PlayerColour } from "../../models/player/player.colour.enum";

export abstract class AbstractBoardGameComponent implements OnInit {

    @Input() owner: Player;
    @Input() board: Array<Array<BoardGame>>;
    @Output() moveEvent =  new EventEmitter<Move>();
    public colour: string;

    ngOnInit(): void {
        this.setColour();
    }

    protected setColour(): void {
        if (this.owner !== undefined && this.owner !== null) {
            if (this.owner.colour === PlayerColour.BLUE) {
                this.colour = "#0275d8";
            } else {
                this.colour = "#d9534f";
            }
        }
    }

}
