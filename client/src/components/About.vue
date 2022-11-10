<script lang="ts" setup>
import Section from './Section.vue'
</script>

<template>
  <Section italic>
    <template v-slot:title>What is a "Fibonacci"?</template>

    "In mathematics, the Fibonacci numbers, commonly denoted F<sub>n</sub>, form
    a sequence, the Fibonacci sequence, in which each number is the sum of the
    two preceding ones."
    <br />
    <a
      href="https://en.wikipedia.org/wiki/Fibonacci_number"
      class="float-right border-b border-cyan-300 text-cyan-300 visited:border-indigo-300 visited:text-indigo-300"
      target="_blank"
      rel="noopener"
    >
      Wikipedia
    </a>
  </Section>
</template>

<style lang="scss" scoped></style>
