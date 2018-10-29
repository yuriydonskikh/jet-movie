export enum SortOptions {
    None,
    ByDate,
    ByDateDesc,
    ByTitle,
    ByTitleDesc,
    ByCountry,
    ByCountryDesc
}
export class MovieRequest {
    public Title: string;
    public Year: number;
    public Genre: string;
    public Description: string;
    public Country: string;
    public Actors: string;
    public SortBy: SortOptions;
    public Page: number;
    public PageSize: number;
}
