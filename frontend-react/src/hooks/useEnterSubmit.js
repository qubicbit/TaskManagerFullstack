import { useCallback } from "react";

export default function useEnterSubmit(onSubmit) {
    return useCallback(
        (e) => {
            if (e.key === "Enter") {
                if (e.shiftKey) return; // ny rad

                e.preventDefault();
                onSubmit();
            }
        },
        [onSubmit]
    );
}
