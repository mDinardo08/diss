import { Component } from "@angular/core";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import { Move } from "../../models/move/move.model";
import { AbstractGameService } from "../../services/game/game.service.abstract";

@Component({
    selector: "game",
    templateUrl: "./game.component.html"
})

export class GameComponent {

    constructor(private gameService: AbstractGameService) {}

    public game: BoardGame;

    public moveMade($event: Move): void {
        this.gameService.makeMove($event).subscribe((res) => {
            this.game.board = res.game;
        });
    }
}
