
export const ShrinkText = (text: string) => text.length <= 60 ? text : (text.substr(0, 60) + "...")