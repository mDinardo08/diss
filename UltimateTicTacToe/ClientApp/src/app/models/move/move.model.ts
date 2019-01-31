import { Point2D } from "../point2D/point2D.model";
import { PlayerColour } from "../player/player.colour.enum";

export class Move {
    next: Move;
    possition: Point2D;
    owner: PlayerColour;
}
