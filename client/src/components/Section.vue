<script lang="ts" setup>
defineProps<{
  /** Makes the description text italic. */
  italic?: boolean
}>()
</script>

<template>
  <section class="flex max-w-full flex-col items-center">
    <h2 class="font-lobster text-3xl text-pink-500">
      <slot name="title"></slot>
    </h2>

    <p class="font-nova mt-2 max-w-md text-fuchsia-300" :class="{ italic }">
      <slot></slot>
    </p>

    <slot name="footer"></slot>
  </section>
</template>

<style lang="scss" scoped></style>
