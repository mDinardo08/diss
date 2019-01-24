import { Move } from "../../models/move/move.model";
import { Player } from "../../models/player/player.model";
import { Observable } from "rxjs/Observable";
import { BoardGameDTO } from "../../models/DTOs/BoardGameDTO";

export abstract class AbstractGameService {

    abstract makeMove(move: Move): Observable<BoardGameDTO>;

    abstract createGame(size: number, players: Array<Player>): Observable<BoardGameDTO>;

    abstract getCurrentPlayer(): Player;
}
