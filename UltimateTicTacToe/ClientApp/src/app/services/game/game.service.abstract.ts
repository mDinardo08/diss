import { Move } from "../../models/move/move.model";
import { Player } from "../../models/player/player.model";
import { Observable } from "rxjs/Observable";
import { BoardGameDTO } from "../../models/DTOs/BoardGameDTO";

export abstract class AbstractGameService {

    abstract makeMove(move: Move): void;

    abstract createGame(size: number, players: Array<Player>): void;

    abstract getCurrentPlayer(): Player;

    abstract getAvailableMoves(): Array<Move>;
}
