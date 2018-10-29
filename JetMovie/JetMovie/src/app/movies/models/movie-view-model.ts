export enum ProvidedBy {
    None,
    Cinemaworld,
    FilmWorld,
    Both
}

export interface MovieViewModel {
    id: string;
    title: string;
    released: Date;
    genres: Array<string>;
    description: string;
    poster: string;
    country: string;
    actors: string;
    providedBy: ProvidedBy;
    price: number;
}