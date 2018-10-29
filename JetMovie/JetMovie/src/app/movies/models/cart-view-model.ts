import { ProvidedBy } from "./movie-view-model";

export class CartViewModel {
    public id: number;
    public movieId: string;
    public movieTitle: string;
    public providedBy: ProvidedBy;
    public price: number;
    public paid: boolean;
}