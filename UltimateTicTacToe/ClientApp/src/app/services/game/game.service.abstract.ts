import { Move } from "../../models/move/move.model";
import { Player } from "../../models/player/player.model";

export abstract class IGameService {

    abstract makeMove(move: Move);

    abstract createGame(size: number, players: Array<Player>);

    abstract getCurrentPlayer();
}
