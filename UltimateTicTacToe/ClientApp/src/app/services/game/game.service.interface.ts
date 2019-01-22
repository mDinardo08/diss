import { Move } from "../../models/move/move.model";
import { Player } from "../../models/player/player.model";

export interface IGameService {

    makeMove(move: Move);

    createGame(size: number, players: Array<Player>);

    getCurrentPlayer();
}
