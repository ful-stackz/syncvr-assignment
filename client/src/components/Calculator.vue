<script lang="ts" setup>
import { ref } from 'vue'
import { useQueriesStore } from '@/stores/queries'
import Section from './Section.vue'

const queriesStore = useQueriesStore()

const value = ref<number>()
const isProcessing = ref(false)
const error = ref<string>()

const submit = async () => {
  const val = value.value

  if (!val && val !== 0) {
    error.value = 'Error: Provide a position'
    return
  }

  if (val < 0) {
    error.value = 'Error: Position must be greater than or equal to 0 (zero)'
    return
  }

  isProcessing.value = true

  try {
    await queriesStore.calculateFibonacciNumber(val)
  } catch (err) {
    // @ts-ignore
    error.value = err.message
  } finally {
    isProcessing.value = false
  }
}
</script>

<template>
  <Section>
    <template v-slot:title>Calculator</template>

    Input the position you want to calculate the number for in the box below and
    click the calculate button.

    <template v-slot:footer>
      <div
        class="mt-4 flex max-w-lg flex-row items-center justify-between rounded-full border border-slate-500 bg-black bg-opacity-30 p-2 lg:text-xl"
      >
        <label for="position" class="font-nova p-2 pl-4 text-slate-400">
          N =
        </label>
        <input
          v-model="value"
          type="number"
          id="position"
          name="position"
          min="0"
          placeholder="5"
          class="font-nova grow border-b border-slate-500 bg-transparent p-2 text-slate-50 outline-none placeholder:text-slate-500 focus:border-slate-100"
        />
        <button
          type="button"
          class="ml-2 rounded-full px-4 py-2 text-slate-50"
          :disabled="isProcessing"
          @click="submit"
        >
          {{ isProcessing ? 'Calculating...' : 'Calculate' }}
        </button>
      </div>

      <p v-if="error" class="text-glow font-nova mt-2 text-red-300">
        {{ error }}
      </p>
    </template>
  </Section>
</template>

<style lang="scss" scoped>
button[type='button'] {
  --background: linear-gradient(
    to bottom right,
    theme(colors.purple.500),
    theme(colors.blue.800)
  );
  background: var(--background);

  --foreground: theme(colors.slate.50);
  color: var(--foreground);

  &:hover {
    --background: linear-gradient(
      to bottom right,
      theme(colors.pink.400),
      theme(colors.blue.700)
    );
  }

  &:disabled {
    --background: theme(colors.neutral.500);
    --foreground: theme(colors.neutral.300);
  }
}
</style>
