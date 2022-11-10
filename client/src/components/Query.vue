<script lang="ts" setup>
import type { FibonacciQuery } from '@/models/fibonacci-query'

const props = defineProps<{ query: FibonacciQuery }>()

const avatarUrl = `https://avatars.dicebear.com/api/bottts/${props.query.clientId}.svg`
</script>

<template>
  <div
    class="flex flex-row items-center rounded-2xl bg-black bg-opacity-25 p-4"
  >
    <img alt="Client avatar" :src="avatarUrl" class="avatar w-12" />
    <div class="overflow-hidden">
      <p class="font-nova ml-4 text-slate-300">
        calculated position {{ query.position }} on
        {{ query.createdAt.toLocaleDateString() }} at
        {{ query.createdAt.toLocaleTimeString() }}
      </p>
      <p class="font-nova ml-4 overflow-hidden text-ellipsis text-slate-400">
        {{ query.value }}
      </p>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.avatar {
  min-width: 3rem;
}
</style>
