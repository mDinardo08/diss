import { Move } from "../../models/move/move.model";
import { Player } from "../../models/player/player.model";
import { Output, EventEmitter } from "@angular/core";
import { BoardGame } from "../../models/boardGame/boardgame/boardgame.model";

export abstract class AbstractGameService {
    @Output() boardUpdatedEvent = new EventEmitter<Array<Array<BoardGame>>>();
    @Output() gameOverEvent = new EventEmitter<Player>();
    abstract makeMove(move: Move): void;

    abstract createGame(size: number, players: Array<Player>): void;

    abstract getCurrentPlayer(): Player;

    abstract getAvailableMoves(): Array<Move>;

    abstract getBoard(): Array<Array<BoardGame>>;

    abstract getLastMove(): Move;
}
