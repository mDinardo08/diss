import { Component } from "@angular/core";
import { GameService } from "../../services";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";
import { Move } from "../../models/move/move.model";

@Component({
    selector: "game",
    templateUrl: "./game.component.html"
})

export class GameComponent {

    constructor(private gameService: GameService) {}

    public game: BoardGame;

    public moveMade($event: Move): void {
        this.gameService.makeMove(this.game.board, $event).subscribe((res) => {
            this.game.board = res.game;
        });
    }
}
