import { Point2D } from "../point2D/point2D.model";
import { Player } from "../player/player.model";

export class Move {
    next: Move;
    possition: Point2D;
    owner: Player;
}
