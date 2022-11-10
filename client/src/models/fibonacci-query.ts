export type FibonacciQueryDbo = {
  id: string
  createdAt: string
  clientId: string
  position: number
  value: number
}

export class FibonacciQuery {
  constructor(
    public id: string,
    public createdAt: Date,
    public clientId: string,
    public position: number,
    public value: number
  ) {}

  static fromJson(data: FibonacciQueryDbo) {
    return new FibonacciQuery(
      data.id,
      new Date(data.createdAt),
      data.clientId,
      data.position,
      data.value
    )
  }
}
