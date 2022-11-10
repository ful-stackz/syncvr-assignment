import { ref, type Ref } from 'vue'
import { defineStore } from 'pinia'
import {
  FibonacciQuery,
  type FibonacciQueryDbo,
} from '@/models/fibonacci-query'
import type { ApiError } from '@/models/error'
import { config } from '@/config'

export const useQueriesStore = defineStore('queries', () => {
  const queries: Ref<FibonacciQuery[]> = ref<FibonacciQuery[]>([])

  async function fetchHistoricalQueries() {
    const response = await fetch(`${config.apiBase}/fibonacci/history`)

    if (response.status === 200) {
      const dbos: FibonacciQueryDbo[] = await response.json()
      const models = dbos.map((dbo) => FibonacciQuery.fromJson(dbo))
      queries.value = models
      return models
    } else {
      const data: ApiError = await response.json()
      throw new Error(data.error)
    }
  }

  async function calculateFibonacciNumber(position: number) {
    const response = await fetch(
      `${config.apiBase}/fibonacci/calculate/${position}`,
      {
        method: 'GET',
        headers: { 'Client-Id': localStorage.getItem('client-id')! },
      }
    )

    if (response.status === 200) {
      const dbo: FibonacciQueryDbo = await response.json()
      const model = FibonacciQuery.fromJson(dbo)
      queries.value.push(model)
      return model
    } else {
      const data: ApiError = await response.json()
      throw new Error(data.error)
    }
  }

  return { queries, fetchHistoricalQueries, calculateFibonacciNumber }
})
