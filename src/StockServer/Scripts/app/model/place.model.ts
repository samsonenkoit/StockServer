export class Place {
    constructor(
        public id: number,
        public name: string,
        public geoLocation: {latitude: number, longitude: number}

    ) { }
}