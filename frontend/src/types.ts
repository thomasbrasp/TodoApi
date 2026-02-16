export interface Character {
  id: number;
  name: {
    first: string;
    middle: string;
    last: string;
  };
  images: {
    main: string;
  };
  sayings: string[];
}
