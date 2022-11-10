<script setup lang="ts">
// @ts-ignore - uuid does not provide ts typings for the latest version
import { v4 as uuidv4 } from 'uuid'
import Backdrop from './components/Backdrop.vue'
import Header from '@/components/Header.vue'
import About from '@/components/About.vue'
import Calculator from '@/components/Calculator.vue'
import History from '@/components/History.vue'

if (!localStorage.getItem('client-id')) {
  localStorage.setItem('client-id', uuidv4())
}
</script>

<template>
  <Backdrop></Backdrop>

  <div
    class="container z-10 mx-auto flex flex-col items-center justify-start px-4 pb-4"
  >
    <Header></Header>
    <About class="mt-8"></About>
    <Calculator class="mt-8"></Calculator>
    <History class="mt-8"></History>
  </div>
</template>

<style lang="scss" scoped></style>
