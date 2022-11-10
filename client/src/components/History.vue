<script lang="ts" setup>
import Section from './Section.vue'
import Query from './Query.vue'
import { useQueriesStore } from '@/stores/queries'
import { computed, onMounted, ref } from 'vue'

const queriesStore = useQueriesStore()

const loading = ref(true)
const error = ref<string>()
const queries = computed(() =>
  queriesStore.queries.sort(
    (a, b) => b.createdAt.getTime() - a.createdAt.getTime()
  )
)

onMounted(async () => {
  try {
    await queriesStore.fetchHistoricalQueries()
  } catch (err) {
    // @ts-ignore
    error.value = err.message
  } finally {
    loading.value = false
  }
})
</script>

<template>
  <Section class="">
    <template v-slot:title>History</template>

    Checkout what Fibonacci numbers other people were interested in.

    <template v-slot:footer>
      <div
        v-if="loading || queries.length === 0"
        class="font-nova mt-4 text-slate-300"
      >
        <template v-if="loading">Loading...</template>
        <template v-else>No queries yet ¯\_(ツ)_/¯</template>
      </div>
      <template v-else>
        <div class="flex max-w-full flex-col">
          <Query
            v-for="query of queries"
            :key="query.id"
            class="mt-4 w-full"
            :query="query"
          ></Query>
        </div>
      </template>
    </template>
  </Section>
</template>

<style lang="scss" scoped></style>
